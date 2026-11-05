use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "_IntegrationToNotificationRule")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false, column_name = "A")]
    pub integration_id: String,
    #[sea_orm(primary_key, auto_increment = false, column_name = "B")]
    pub notification_rule_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::integration::Entity",
        from = "Column::IntegrationId",
        to = "super::integration::Column::Id"
    )]
    Integration,
    #[sea_orm(
        belongs_to = "super::notification_rule::Entity",
        from = "Column::NotificationRuleId",
        to = "super::notification_rule::Column::Id"
    )]
    NotificationRule,
}

impl ActiveModelBehavior for ActiveModel {}
