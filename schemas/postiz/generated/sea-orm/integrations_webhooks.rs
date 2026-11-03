use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "IntegrationsWebhooks")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "integrationId")]
    pub integration_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "webhookId")]
    pub webhook_id: String,
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
        belongs_to = "super::webhooks::Entity",
        from = "Column::WebhookId",
        to = "super::webhooks::Column::Id"
    )]
    Webhook,
}

impl Related<super::integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Integration.def()
    }
}

impl Related<super::webhooks::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Webhook.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
