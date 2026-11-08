use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "revenuecat_webhook_events")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "eventType")]
    pub event_type: String,
    #[sea_orm(column_name = "eventTimestamp")]
    pub event_timestamp: DateTime,
    #[sea_orm(column_name = "appUserId")]
    pub app_user_id: String,
    pub environment: String,
    #[sea_orm(column_name = "productId")]
    pub product_id: Option<String>,
    #[sea_orm(column_name = "transactionId")]
    pub transaction_id: Option<String>,
    #[sea_orm(column_name = "originalTransactionId")]
    pub original_transaction_id: Option<String>,
    #[sea_orm(column_name = "entitlementIds")]
    pub entitlement_ids: Option<String>,
    #[sea_orm(default_value = false)]
    pub processed: bool,
    #[sea_orm(column_name = "processingError", column_type = "Text")]
    pub processing_error: Option<String>,
    #[sea_orm(column_name = "retryCount", default_value = 0)]
    pub retry_count: i32,
    #[sea_orm(column_name = "rawEventData")]
    pub raw_event_data: Json,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
