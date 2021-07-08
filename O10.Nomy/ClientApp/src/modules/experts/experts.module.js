"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.ExpertsModule = void 0;
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var platform_browser_1 = require("@angular/platform-browser");
var animations_1 = require("@angular/platform-browser/animations");
var http_1 = require("@angular/common/http");
var router_1 = require("@angular/router");
var bottom_sheet_1 = require("@angular/material/bottom-sheet");
var button_1 = require("@angular/material/button");
var button_toggle_1 = require("@angular/material/button-toggle");
var card_1 = require("@angular/material/card");
var checkbox_1 = require("@angular/material/checkbox");
var dialog_1 = require("@angular/material/dialog");
var divider_1 = require("@angular/material/divider");
var expansion_1 = require("@angular/material/expansion");
var form_field_1 = require("@angular/material/form-field");
var icon_1 = require("@angular/material/icon");
var input_1 = require("@angular/material/input");
var list_1 = require("@angular/material/list");
var progress_bar_1 = require("@angular/material/progress-bar");
var radio_1 = require("@angular/material/radio");
var select_1 = require("@angular/material/select");
var slide_toggle_1 = require("@angular/material/slide-toggle");
var stepper_1 = require("@angular/material/stepper");
var expert_chat_component_1 = require("./expert-chat/expert-chat.component");
var experts_list_component_1 = require("./experts-list/experts-list.component");
var ExpertsModule = /** @class */ (function () {
    function ExpertsModule() {
    }
    ExpertsModule = __decorate([
        core_1.NgModule({
            declarations: [
                experts_list_component_1.ExpertsListComponent,
                expert_chat_component_1.ExpertChatComponent
            ],
            imports: [
                common_1.CommonModule, platform_browser_1.BrowserModule, animations_1.BrowserAnimationsModule, http_1.HttpClientModule,
                expansion_1.MatExpansionModule, input_1.MatInputModule, select_1.MatSelectModule, dialog_1.MatDialogModule, button_1.MatButtonModule, bottom_sheet_1.MatBottomSheetModule, card_1.MatCardModule, icon_1.MatIconModule, progress_bar_1.MatProgressBarModule, list_1.MatListModule, button_toggle_1.MatButtonToggleModule, divider_1.MatDividerModule, stepper_1.MatStepperModule, checkbox_1.MatCheckboxModule, radio_1.MatRadioModule, form_field_1.MatFormFieldModule, slide_toggle_1.MatSlideToggleModule,
                router_1.RouterModule.forRoot([
                    { path: 'expert-chat/:userId/:id', component: expert_chat_component_1.ExpertChatComponent },
                    { path: 'experts-list/:userId', component: experts_list_component_1.ExpertsListComponent }
                ])
            ]
        })
    ], ExpertsModule);
    return ExpertsModule;
}());
exports.ExpertsModule = ExpertsModule;
//# sourceMappingURL=experts.module.js.map