using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static bool isFirstNeg=false;
    public static bool isSecondNeg=false;
    public static bool isFirstDec=false;
    public static bool isSecondDec=false;
    public static bool isFirstTerm=true;
    public static bool isSecondTerm=false;
    public static bool isNumLastProcess=true;
    public static bool isDBZ=false;//divide by zero
    public static String firstTerm="0",secondTerm="";
    public static String op;//null check intuitive

    
    public MainWindow()
    {
        InitializeComponent();
    }
    private void reset(){
        isFirstNeg=false;
        isSecondNeg=false;
        isFirstDec=false;
        isSecondDec=false;
        isFirstTerm=true;
        isSecondTerm=false;
        isNumLastProcess=true;
        isDBZ=false;
        firstTerm="0";
        secondTerm="";
        op=null;
        Active_Input.Text="0";
        Previous_Expression.Text="";
    }
    private void process_Click(object sender, RoutedEventArgs e)
    {
        FrameworkElement sourceFrameworkElement = e.Source as FrameworkElement;
        switch (sourceFrameworkElement.Name)
        {
            // Numbers
            case "button_0":
                if (isDBZ){
                    reset();
                }
                if (Active_Input.Text!="0"){
                    if(isNumLastProcess){
                    Active_Input.Text+="0";
                    }
                    else{
                        Active_Input.Text="0";
                    }
                }
                isNumLastProcess=true;
                break;
            case "button_1":
            case "button_2":
            case "button_3":
            case "button_4":
            case "button_5":
            case "button_6":
            case "button_7":
            case "button_8":
            case "button_9":
                string digit = sourceFrameworkElement.Name;
                digit=digit[digit.Length-1].ToString();
                if (isDBZ || (Active_Input.Text=="0" && isFirstTerm) || (Previous_Expression.Text!= "" && Previous_Expression.Text[Previous_Expression.Text.Length-1]=='=' && firstTerm.Length!=0 && secondTerm.Length!=0))
                {
                    reset();
                    Active_Input.Text=digit;
                }
                else if(!isNumLastProcess || (Active_Input.Text=="0" && isSecondTerm))
                {
                    Active_Input.Text=digit;
                }
                else
                {
                    Active_Input.Text+=digit;
                }
                isNumLastProcess=true;
                break;
            // Operations
            case "button_plus":
                if(isDBZ){
                    break;
                }
                if(isNumLastProcess)
                {
                    if(isFirstTerm)
                    {
                    Previous_Expression.Text=Active_Input.Text+"+";
                    isFirstTerm=false;
                    isSecondTerm=true;
                    firstTerm=Active_Input.Text;
                    }
                    isNumLastProcess=false;
                }
                else{
                    Previous_Expression.Text=Previous_Expression.Text.Remove(Previous_Expression.Text.Length-1,1)+"+";
                }
                op="+";
                break;
            case "button_minus":
                if(isDBZ){
                    break;
                }
                if(isNumLastProcess)
                {
                    if(isFirstTerm)
                    {
                    Previous_Expression.Text=Active_Input.Text+"-";
                    isSecondTerm=true;
                    isFirstTerm=false;
                    firstTerm=Active_Input.Text;
                    }
                    isNumLastProcess=false;
                }
                else{
                    Previous_Expression.Text=Previous_Expression.Text.Remove(Previous_Expression.Text.Length-1,1)+"-";
                }
                op="-";
                break;
            case "button_mult":
                if(isDBZ){
                    break;
                }
                if(isNumLastProcess)
                {
                    if(isFirstTerm)
                    {
                    Previous_Expression.Text=Active_Input.Text+"×";
                    isSecondTerm=true;
                    isFirstTerm=false;
                    firstTerm=Active_Input.Text;
                    }
                    isNumLastProcess=false;
                }
                else{
                    Previous_Expression.Text=Previous_Expression.Text.Remove(Previous_Expression.Text.Length-1,1)+"×";
                }
                op="×";
                break;
            case "button_div":
                if(isDBZ){
                    break;
                }
                if(isNumLastProcess)
                {
                    if(isFirstTerm)
                    {
                    Previous_Expression.Text=Active_Input.Text+"÷";
                    isSecondTerm=true;
                    isFirstTerm=false;
                    firstTerm=Active_Input.Text;
                    }
                    else if(isSecondTerm){
                        if (Active_Input.Text=="0")
                            Active_Input.Text="Cannot divide by zero";
                    }
                    isNumLastProcess=false;
                }
                else{
                    Previous_Expression.Text=Previous_Expression.Text.Remove(Previous_Expression.Text.Length-1,1)+"÷";
                }
                op="÷";
                break;
            case "button_percent":
                if(isDBZ){
                    break;
                }
                break;
            // Advanced Operations
            case "button_fraction":
                if(isDBZ){
                    break;
                }
                break;
            case "button_square":
                if(isDBZ){
                    break;
                }
                break;
            case "button_root":
                if(isDBZ){
                    break;
                }
                break;
            // Clear/Backspace
            case "button_CE":// removes current entry
                Active_Input.Text="0";
                if(!isSecondTerm){
                    Previous_Expression.Text="";
                    isFirstDec=false;
                    isFirstNeg=false;
                }
                isSecondDec=false;
                isSecondNeg=false;
                isNumLastProcess=true;
                isDBZ=false;
                break;
            case "button_C":// removes everything
                reset();
                break;
            case "button_backspace":
                if(isDBZ){
                    reset();
                    break;
                }
                if (Active_Input.Text.Length!=0)
                {
                    if(Active_Input.Text.Length==3 && Active_Input.Text[1]=='0' && ((isFirstNeg && isFirstDec)||(isSecondNeg && isSecondDec))){
                        Active_Input.Text="0";
                        if (isFirstNeg && isFirstDec){
                            isFirstDec=false;
                            isFirstNeg=false;
                        }
                        else if(isSecondDec && isSecondNeg){
                            isSecondDec=false;
                            isSecondNeg=false;
                        }
                        break;
                    }
                    Active_Input.Text=Active_Input.Text.Remove(Active_Input.Text.Length-1,1);
                }
                if (Active_Input.Text.Length==0)
                    Active_Input.Text="0";
                break;    
            // Formatting
            case "button_decimal":
                if ((isFirstTerm && !isFirstDec) || (isSecondTerm && !isSecondDec) && Active_Input.Text[Active_Input.Text.Length-1]!='.')
                    Active_Input.Text+=".";
                    if (isFirstTerm)
                        isFirstDec=true;
                    if (isSecondTerm)
                        isSecondDec=true;
                    // isNumLastProcess=false;
                break;
            case "button_sign":
                if (Active_Input.Text[0]!='0' && Active_Input.Text[0]!='-')
                {
                    Active_Input.Text="-"+Active_Input.Text;
                    if (isFirstTerm)
                        isFirstNeg=true;
                    if (isSecondTerm)
                        isSecondNeg=true;
                }
                else if (Active_Input.Text[0]=='-')
                {
                    Active_Input.Text=Active_Input.Text.Remove(0,1);
                    if (isFirstTerm)
                        isFirstNeg=false;
                    if (isSecondTerm)
                        isSecondNeg=false;
                }
                break;
            // Evaluate
            case "button_equal"://technically equals generates a number when pressed
                if(isDBZ){
                    reset();
                    break;
                }
                if(isFirstTerm)
                {
                    Previous_Expression.Text=Active_Input.Text+"=";
                }
                else
                {
                    secondTerm=Active_Input.Text;
                    Previous_Expression.Text+=secondTerm+"=";
                    if(isFirstDec || isSecondDec)
                    {
                        decimal first=decimal.Parse(firstTerm);
                        decimal second=decimal.Parse(secondTerm);
                        decimal result=0;
                        switch(op){
                            case "+":
                                result = first + second;
                                break;
                            case "-":
                                result = first - second;
                                break;
                            case "×":
                                result = first * second;
                                break;
                            case "÷"://build case for division by zero
                                if (second==0.0m){//check this for decimal
                                    isDBZ=true;
                                    break;
                                }
                                result = first / second;
                                break;
                        }
                        if (isDBZ){
                            if (first==0){//fix this for decimal
                                Active_Input.Text="Result is undefined";    
                            }
                            else{
                                Active_Input.Text="Cannot divide by zero";
                            }
                        }
                        else{
                            Active_Input.Text=result.ToString();
                        }
                    }
                    else{
                        Int64 first=Int64.Parse(firstTerm);
                        Int64 second=Int64.Parse(secondTerm);
                        Int64 result=0;
                        decimal dresult=0.0m;
                        bool divide = false;
                        switch(op){
                            case "+":
                                result = first + second;
                                break;
                            case "-":
                                result = first - second;
                                break;
                            case "×":
                                result = first * second;
                                break;
                            case "÷":
                                if (second==0){
                                    isDBZ=true;
                                    break;
                                }
                                dresult = Decimal.Divide(first, second);
                                divide=true;
                                break;
                                
                        }
                        if (isDBZ){
                            if (first==0){//fix this for decimal
                                Active_Input.Text="Result is undefined";    
                            }
                            else{
                                Active_Input.Text="Cannot divide by zero";
                            }
                        }
                        else{
                            if(divide){
                                Active_Input.Text=dresult.ToString();
                                isFirstDec=true;
                            }
                            else{
                                Active_Input.Text=result.ToString();
                            }
                        }
                    }
                    isFirstTerm=true;
                    isSecondTerm=false;
                    // add correct decimal and negation code here.
                }
                isNumLastProcess=true;
                break;
        }
        e.Handled=true;
    }
}