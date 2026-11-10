use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "User")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(unique)]
    pub email: Option<String>,
    #[sea_orm(unique)]
    pub username: Option<String>,
    #[sea_orm(column_name = "isAdmin", default_value = false)]
    pub is_admin: bool,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "paymentProcessorUserId")]
    pub payment_processor_user_id: Option<String>,
    #[sea_orm(column_name = "subscriptionStatus")]
    pub subscription_status: Option<String>,
    #[sea_orm(column_name = "subscriptionPlan")]
    pub subscription_plan: Option<String>,
    #[sea_orm(column_name = "datePaid")]
    pub date_paid: Option<DateTime>,
    #[sea_orm(default_value = 3)]
    pub credits: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::gpt_response::Entity")]
    GptResponses,
    #[sea_orm(has_many = "super::contact_form_message::Entity")]
    ContactFormMessages,
    #[sea_orm(has_many = "super::task::Entity")]
    Tasks,
    #[sea_orm(has_many = "super::file::Entity")]
    Files,
}

impl Related<super::gpt_response::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GptResponses.def()
    }
}

impl Related<super::contact_form_message::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ContactFormMessages.def()
    }
}

impl Related<super::task::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tasks.def()
    }
}

impl Related<super::file::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Files.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
