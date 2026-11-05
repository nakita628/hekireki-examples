use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LoopsEmailSource")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "emailMessageId")]
    pub email_message_id: Option<String>,
    #[sea_orm(column_name = "loopId")]
    pub loop_id: Option<String>,
    #[sea_orm(column_name = "loopName")]
    pub loop_name: Option<String>,
    #[sea_orm(column_name = "campaignId")]
    pub campaign_id: Option<String>,
    #[sea_orm(column_name = "campaignName")]
    pub campaign_name: Option<String>,
    #[sea_orm(column_name = "sourceType")]
    pub source_type: Option<String>,
    #[sea_orm(column_name = "eventTime")]
    pub event_time: Option<DateTime>,
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
