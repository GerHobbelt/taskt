﻿using System;
using System.Linq;
using System.Xml.Serialization;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using taskt.UI.Forms;
using taskt.UI.CustomControls;

namespace taskt.Core.Automation.Commands
{
    [Serializable]
    [Attributes.ClassAttributes.Group("Dictionary Commands")]
    [Attributes.ClassAttributes.Description("This command allows you to get Keys List in Dictionary")]
    [Attributes.ClassAttributes.UsesDescription("Use this command when you want to get Keys List in Dictionary.")]
    [Attributes.ClassAttributes.ImplementationDescription("")]
    public class GetDictionaryKeysListCommand : ScriptCommand
    {
        [XmlAttribute]
        [Attributes.PropertyAttributes.PropertyDescription("Please input The Dictionary Variable")]
        [Attributes.PropertyAttributes.PropertyUIHelper(Attributes.PropertyAttributes.PropertyUIHelper.UIAdditionalHelperType.ShowVariableHelper)]
        [Attributes.PropertyAttributes.InputSpecification("Enter a string of comma seperated values.")]
        [Attributes.PropertyAttributes.SampleUsage("**myDictionary** or **{{{vMyDic}}}**")]
        [Attributes.PropertyAttributes.Remarks("")]
        [Attributes.PropertyAttributes.PropertyShowSampleUsageInDescription(true)]
        [Attributes.PropertyAttributes.PropertyRecommendedUIControl(Attributes.PropertyAttributes.PropertyRecommendedUIControl.RecommendeUIControlType.ComboBox)]
        [Attributes.PropertyAttributes.PropertyInstanceType(Attributes.PropertyAttributes.PropertyInstanceType.InstanceType.Dictionary)]
        public string v_InputData { get; set; }

        [XmlAttribute]
        [Attributes.PropertyAttributes.PropertyDescription("Please indicate the variable to apply keys list")]
        [Attributes.PropertyAttributes.InputSpecification("Enter a unique dataset name that will be used later to traverse over the data")]
        [Attributes.PropertyAttributes.SampleUsage("**vMyData** or **{{{vMyData}}}**")]
        [Attributes.PropertyAttributes.Remarks("")]
        [Attributes.PropertyAttributes.PropertyShowSampleUsageInDescription(true)]
        [Attributes.PropertyAttributes.PropertyUIHelper(Attributes.PropertyAttributes.PropertyUIHelper.UIAdditionalHelperType.ShowVariableHelper)]
        [Attributes.PropertyAttributes.PropertyRecommendedUIControl(Attributes.PropertyAttributes.PropertyRecommendedUIControl.RecommendeUIControlType.ComboBox)]
        [Attributes.PropertyAttributes.PropertyIsVariablesList(true)]
        public string v_OutputVariable { get; set; }

        public GetDictionaryKeysListCommand()
        {
            this.CommandName = "GetDictionaryKeysListCommand";
            this.SelectionName = "Get Dictionary Keys List";
            this.CommandEnabled = true;
            this.CustomRendering = true;
        }

        public override void RunCommand(object sender)
        {
            var engine = (Core.Automation.Engine.AutomationEngineInstance)sender;
            
            Dictionary<string, string> dic = (Dictionary<string, string>)v_InputData.GetRawVariable(engine).VariableValue;
            dic.Keys.ToList().StoreInUserVariable(engine, v_OutputVariable);
        }
        
        public override List<Control> Render(frmCommandEditor editor)
        {
            base.Render(editor);

            var ctrls = CommandControls.MultiCreateInferenceDefaultControlGroupFor(this, editor);
            RenderedControls.AddRange(ctrls);

            return RenderedControls;
        }

        public override string GetDisplayValue()
        {
            return base.GetDisplayValue() + $" [From: {v_InputData}, Store In: {v_OutputVariable}]";
        }

        public override bool IsValidate(frmCommandEditor editor)
        {
            if (String.IsNullOrEmpty(v_InputData))
            {
                this.IsValid = false;
                this.validationResult += "Dictionary Variable Name is empty.\n";
            }
            if (String.IsNullOrEmpty(v_OutputVariable))
            {
                this.IsValid = false;
                this.validationResult += "Variable is empty.\n";
            }

            return this.IsValid;
        }
    }
}