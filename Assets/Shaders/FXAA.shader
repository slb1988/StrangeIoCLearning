Shader "Hidden/T5FXAA" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX

in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
out highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec2 tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = _glesMultiTexCoord0.xy;
  tmpvar_1 = tmpvar_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp vec4 _MainTex_TexelSize;
in highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec2 rcpFrame_1;
  rcpFrame_1 = _MainTex_TexelSize.xy;
  highp vec3 tmpvar_2;
  highp float lumaEndP_3;
  highp float lumaEndN_4;
  highp vec2 offNP_5;
  highp vec2 posP_6;
  highp vec2 posN_7;
  highp float gradientN_8;
  highp float lengthSign_9;
  highp float lumaS_10;
  highp float lumaN_11;
  highp vec4 tmpvar_12;
  highp vec4 tmpvar_13;
  tmpvar_13.zw = vec2(0.0, 0.0);
  tmpvar_13.xy = (xlv_TEXCOORD0 + (vec2(0.0, -1.0) * _MainTex_TexelSize.xy));
  lowp vec4 tmpvar_14;
  tmpvar_14 = textureLod (_MainTex, tmpvar_13.xy, 0.0);
  tmpvar_12 = tmpvar_14;
  highp vec4 tmpvar_15;
  highp vec4 tmpvar_16;
  tmpvar_16.zw = vec2(0.0, 0.0);
  tmpvar_16.xy = (xlv_TEXCOORD0 + (vec2(-1.0, 0.0) * _MainTex_TexelSize.xy));
  lowp vec4 tmpvar_17;
  tmpvar_17 = textureLod (_MainTex, tmpvar_16.xy, 0.0);
  tmpvar_15 = tmpvar_17;
  highp vec4 tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = textureLod (_MainTex, xlv_TEXCOORD0, 0.0);
  tmpvar_18 = tmpvar_19;
  highp vec4 tmpvar_20;
  highp vec4 tmpvar_21;
  tmpvar_21.zw = vec2(0.0, 0.0);
  tmpvar_21.xy = (xlv_TEXCOORD0 + (vec2(1.0, 0.0) * _MainTex_TexelSize.xy));
  lowp vec4 tmpvar_22;
  tmpvar_22 = textureLod (_MainTex, tmpvar_21.xy, 0.0);
  tmpvar_20 = tmpvar_22;
  highp vec4 tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24.zw = vec2(0.0, 0.0);
  tmpvar_24.xy = (xlv_TEXCOORD0 + (vec2(0.0, 1.0) * _MainTex_TexelSize.xy));
  lowp vec4 tmpvar_25;
  tmpvar_25 = textureLod (_MainTex, tmpvar_24.xy, 0.0);
  tmpvar_23 = tmpvar_25;
  highp float tmpvar_26;
  tmpvar_26 = ((tmpvar_12.y * 1.96321) + tmpvar_12.x);
  lumaN_11 = tmpvar_26;
  highp float tmpvar_27;
  tmpvar_27 = ((tmpvar_15.y * 1.96321) + tmpvar_15.x);
  highp float tmpvar_28;
  tmpvar_28 = ((tmpvar_18.y * 1.96321) + tmpvar_18.x);
  highp float tmpvar_29;
  tmpvar_29 = ((tmpvar_20.y * 1.96321) + tmpvar_20.x);
  highp float tmpvar_30;
  tmpvar_30 = ((tmpvar_23.y * 1.96321) + tmpvar_23.x);
  lumaS_10 = tmpvar_30;
  highp float tmpvar_31;
  tmpvar_31 = max (tmpvar_28, max (max (tmpvar_26, tmpvar_27), max (tmpvar_30, tmpvar_29)));
  highp float tmpvar_32;
  tmpvar_32 = (tmpvar_31 - min (tmpvar_28, min (min (tmpvar_26, tmpvar_27), min (tmpvar_30, tmpvar_29))));
  highp float tmpvar_33;
  tmpvar_33 = max (0.0833333, (tmpvar_31 * 0.25));
  if ((tmpvar_32 < tmpvar_33)) {
    tmpvar_2 = tmpvar_18.xyz;
  } else {
    highp vec3 tmpvar_34;
    tmpvar_34 = (((((tmpvar_12.xyz + tmpvar_15.xyz) + tmpvar_20.xyz) + tmpvar_23.xyz) + tmpvar_18.xyz) * vec3(0.2, 0.2, 0.2));
    highp float tmpvar_35;
    tmpvar_35 = min (0.25, (max (0.0, ((abs((((((tmpvar_26 + tmpvar_27) + tmpvar_29) + tmpvar_30) * 0.25) - tmpvar_28)) / tmpvar_32) - 0.25)) * 1.33333));
    highp vec4 tmpvar_36;
    highp vec4 tmpvar_37;
    tmpvar_37.zw = vec2(0.0, 0.0);
    tmpvar_37.xy = (xlv_TEXCOORD0 + (vec2(-1.0, -1.0) * _MainTex_TexelSize.xy));
    lowp vec4 tmpvar_38;
    tmpvar_38 = textureLod (_MainTex, tmpvar_37.xy, 0.0);
    tmpvar_36 = tmpvar_38;
    highp vec4 tmpvar_39;
    highp vec4 tmpvar_40;
    tmpvar_40.zw = vec2(0.0, 0.0);
    tmpvar_40.xy = (xlv_TEXCOORD0 + (vec2(1.0, -1.0) * _MainTex_TexelSize.xy));
    lowp vec4 tmpvar_41;
    tmpvar_41 = textureLod (_MainTex, tmpvar_40.xy, 0.0);
    tmpvar_39 = tmpvar_41;
    highp vec4 tmpvar_42;
    highp vec4 tmpvar_43;
    tmpvar_43.zw = vec2(0.0, 0.0);
    tmpvar_43.xy = (xlv_TEXCOORD0 + (vec2(-1.0, 1.0) * _MainTex_TexelSize.xy));
    lowp vec4 tmpvar_44;
    tmpvar_44 = textureLod (_MainTex, tmpvar_43.xy, 0.0);
    tmpvar_42 = tmpvar_44;
    highp vec4 tmpvar_45;
    highp vec4 tmpvar_46;
    tmpvar_46.zw = vec2(0.0, 0.0);
    tmpvar_46.xy = (xlv_TEXCOORD0 + _MainTex_TexelSize.xy);
    lowp vec4 tmpvar_47;
    tmpvar_47 = textureLod (_MainTex, tmpvar_46.xy, 0.0);
    tmpvar_45 = tmpvar_47;
    highp float tmpvar_48;
    tmpvar_48 = ((tmpvar_36.y * 1.96321) + tmpvar_36.x);
    highp float tmpvar_49;
    tmpvar_49 = ((tmpvar_39.y * 1.96321) + tmpvar_39.x);
    highp float tmpvar_50;
    tmpvar_50 = ((tmpvar_42.y * 1.96321) + tmpvar_42.x);
    highp float tmpvar_51;
    tmpvar_51 = ((tmpvar_45.y * 1.96321) + tmpvar_45.x);
    bool tmpvar_52;
    tmpvar_52 = (((abs((((0.25 * tmpvar_48) + (-0.5 * tmpvar_27)) + (0.25 * tmpvar_50))) + abs((((0.5 * tmpvar_26) + (-1.0 * tmpvar_28)) + (0.5 * tmpvar_30)))) + abs((((0.25 * tmpvar_49) + (-0.5 * tmpvar_29)) + (0.25 * tmpvar_51)))) >= ((abs((((0.25 * tmpvar_48) + (-0.5 * tmpvar_26)) + (0.25 * tmpvar_49))) + abs((((0.5 * tmpvar_27) + (-1.0 * tmpvar_28)) + (0.5 * tmpvar_29)))) + abs((((0.25 * tmpvar_50) + (-0.5 * tmpvar_30)) + (0.25 * tmpvar_51)))));
    highp float tmpvar_53;
    if (tmpvar_52) {
      tmpvar_53 = -(_MainTex_TexelSize.y);
    } else {
      tmpvar_53 = -(_MainTex_TexelSize.x);
    };
    lengthSign_9 = tmpvar_53;
    if (!(tmpvar_52)) {
      lumaN_11 = tmpvar_27;
    };
    if (!(tmpvar_52)) {
      lumaS_10 = tmpvar_29;
    };
    highp float tmpvar_54;
    tmpvar_54 = abs((lumaN_11 - tmpvar_28));
    gradientN_8 = tmpvar_54;
    highp float tmpvar_55;
    tmpvar_55 = abs((lumaS_10 - tmpvar_28));
    lumaN_11 = ((lumaN_11 + tmpvar_28) * 0.5);
    highp float tmpvar_56;
    tmpvar_56 = ((lumaS_10 + tmpvar_28) * 0.5);
    lumaS_10 = tmpvar_56;
    bool tmpvar_57;
    tmpvar_57 = (tmpvar_54 >= tmpvar_55);
    if (!(tmpvar_57)) {
      lumaN_11 = tmpvar_56;
    };
    if (!(tmpvar_57)) {
      gradientN_8 = tmpvar_55;
    };
    if (!(tmpvar_57)) {
      lengthSign_9 = (tmpvar_53 * -1.0);
    };
    highp float tmpvar_58;
    if (tmpvar_52) {
      tmpvar_58 = 0.0;
    } else {
      tmpvar_58 = (lengthSign_9 * 0.5);
    };
    posN_7.x = (xlv_TEXCOORD0.x + tmpvar_58);
    highp float tmpvar_59;
    if (tmpvar_52) {
      tmpvar_59 = (lengthSign_9 * 0.5);
    } else {
      tmpvar_59 = 0.0;
    };
    posN_7.y = (xlv_TEXCOORD0.y + tmpvar_59);
    gradientN_8 = (gradientN_8 * 0.25);
    posP_6 = posN_7;
    highp vec2 tmpvar_60;
    if (tmpvar_52) {
      highp vec2 tmpvar_61;
      tmpvar_61.y = 0.0;
      tmpvar_61.x = rcpFrame_1.x;
      tmpvar_60 = tmpvar_61;
    } else {
      highp vec2 tmpvar_62;
      tmpvar_62.x = 0.0;
      tmpvar_62.y = rcpFrame_1.y;
      tmpvar_60 = tmpvar_62;
    };
    posN_7 = (posN_7 + (tmpvar_60 * vec2(-1.5, -1.5)));
    posP_6 = (posP_6 + (tmpvar_60 * vec2(1.5, 1.5)));
    offNP_5 = (tmpvar_60 * vec2(2.0, 2.0));
    highp vec4 tmpvar_63;
    lowp vec4 tmpvar_64;
    tmpvar_64 = textureGrad (_MainTex, posN_7, offNP_5, offNP_5);
    tmpvar_63 = tmpvar_64;
    lumaEndN_4 = ((tmpvar_63.y * 1.96321) + tmpvar_63.x);
    highp vec4 tmpvar_65;
    lowp vec4 tmpvar_66;
    tmpvar_66 = textureGrad (_MainTex, posP_6, offNP_5, offNP_5);
    tmpvar_65 = tmpvar_66;
    lumaEndP_3 = ((tmpvar_65.y * 1.96321) + tmpvar_65.x);
    bool tmpvar_67;
    highp float tmpvar_68;
    tmpvar_68 = abs((lumaEndN_4 - lumaN_11));
    tmpvar_67 = (tmpvar_68 >= gradientN_8);
    bool tmpvar_69;
    highp float tmpvar_70;
    tmpvar_70 = abs((lumaEndP_3 - lumaN_11));
    tmpvar_69 = (tmpvar_70 >= gradientN_8);
    if (!((tmpvar_67 && tmpvar_69))) {
      if (!(tmpvar_67)) {
        posN_7 = (posN_7 - offNP_5);
      };
      if (!(tmpvar_69)) {
        posP_6 = (posP_6 + offNP_5);
      };
      if (!(tmpvar_67)) {
        highp vec4 tmpvar_71;
        lowp vec4 tmpvar_72;
        tmpvar_72 = textureGrad (_MainTex, posN_7, offNP_5, offNP_5);
        tmpvar_71 = tmpvar_72;
        lumaEndN_4 = ((tmpvar_71.y * 1.96321) + tmpvar_71.x);
      };
      if (!(tmpvar_69)) {
        highp vec4 tmpvar_73;
        lowp vec4 tmpvar_74;
        tmpvar_74 = textureGrad (_MainTex, posP_6, offNP_5, offNP_5);
        tmpvar_73 = tmpvar_74;
        lumaEndP_3 = ((tmpvar_73.y * 1.96321) + tmpvar_73.x);
      };
      bool tmpvar_75;
      if (tmpvar_67) {
        tmpvar_75 = bool(1);
      } else {
        highp float tmpvar_76;
        tmpvar_76 = abs((lumaEndN_4 - lumaN_11));
        tmpvar_75 = (tmpvar_76 >= gradientN_8);
      };
      bool tmpvar_77;
      if (tmpvar_69) {
        tmpvar_77 = bool(1);
      } else {
        highp float tmpvar_78;
        tmpvar_78 = abs((lumaEndP_3 - lumaN_11));
        tmpvar_77 = (tmpvar_78 >= gradientN_8);
      };
      if (!((tmpvar_75 && tmpvar_77))) {
        if (!(tmpvar_75)) {
          posN_7 = (posN_7 - offNP_5);
        };
        if (!(tmpvar_77)) {
          posP_6 = (posP_6 + offNP_5);
        };
      };
    };
    highp float tmpvar_79;
    if (tmpvar_52) {
      tmpvar_79 = (xlv_TEXCOORD0.x - posN_7.x);
    } else {
      tmpvar_79 = (xlv_TEXCOORD0.y - posN_7.y);
    };
    highp float tmpvar_80;
    if (tmpvar_52) {
      tmpvar_80 = (posP_6.x - xlv_TEXCOORD0.x);
    } else {
      tmpvar_80 = (posP_6.y - xlv_TEXCOORD0.y);
    };
    bool tmpvar_81;
    tmpvar_81 = (tmpvar_79 < tmpvar_80);
    highp float tmpvar_82;
    if (tmpvar_81) {
      tmpvar_82 = lumaEndN_4;
    } else {
      tmpvar_82 = lumaEndP_3;
    };
    lumaEndN_4 = tmpvar_82;
    if ((((tmpvar_28 - lumaN_11) < 0.0) == ((tmpvar_82 - lumaN_11) < 0.0))) {
      lengthSign_9 = 0.0;
    };
    highp float tmpvar_83;
    tmpvar_83 = (tmpvar_80 + tmpvar_79);
    highp float tmpvar_84;
    if (tmpvar_81) {
      tmpvar_84 = tmpvar_79;
    } else {
      tmpvar_84 = tmpvar_80;
    };
    highp float tmpvar_85;
    tmpvar_85 = ((0.5 + (tmpvar_84 * (-1.0 / tmpvar_83))) * lengthSign_9);
    highp float tmpvar_86;
    if (tmpvar_52) {
      tmpvar_86 = 0.0;
    } else {
      tmpvar_86 = tmpvar_85;
    };
    highp float tmpvar_87;
    if (tmpvar_52) {
      tmpvar_87 = tmpvar_85;
    } else {
      tmpvar_87 = 0.0;
    };
    highp vec2 tmpvar_88;
    tmpvar_88.x = (xlv_TEXCOORD0.x + tmpvar_86);
    tmpvar_88.y = (xlv_TEXCOORD0.y + tmpvar_87);
    highp vec4 tmpvar_89;
    lowp vec4 tmpvar_90;
    tmpvar_90 = textureLod (_MainTex, tmpvar_88, 0.0);
    tmpvar_89 = tmpvar_90;
    highp vec3 tmpvar_91;
    tmpvar_91.x = -(tmpvar_35);
    tmpvar_91.y = -(tmpvar_35);
    tmpvar_91.z = -(tmpvar_35);
    tmpvar_2 = ((tmpvar_91 * tmpvar_89.xyz) + ((tmpvar_34 * vec3(tmpvar_35)) + tmpvar_89.xyz));
  };
  highp vec4 tmpvar_92;
  tmpvar_92.w = 0.0;
  tmpvar_92.xyz = tmpvar_2;
  _glesFragData[0] = tmpvar_92;
}



#endif"
}
}
Program "fp" {
SubProgram "gles3 " {
"!!GLES3"
}
}
 }
}
Fallback "Hidden/FXAA II"
}