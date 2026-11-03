use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "users")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub password: Option<String>,
    pub salt: Option<String>,
    #[sea_orm(unique)]
    pub phone: Option<String>,
    #[sea_orm(unique)]
    pub email: String,
    pub avatar: Option<String>,
    pub is_system: Option<bool>,
    pub is_admin: Option<bool>,
    pub is_trial_used: Option<bool>,
    pub lang: Option<String>,
    pub notify_meta: Option<String>,
    pub last_sign_time: Option<DateTime>,
    pub deactivated_time: Option<DateTime>,
    pub created_time: DateTime,
    pub deleted_time: Option<DateTime>,
    pub last_modified_time: Option<DateTime>,
    pub permanent_deleted_time: Option<DateTime>,
    pub ref_meta: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::account::Entity")]
    Accounts,
    #[sea_orm(has_many = "super::user_base_order::Entity")]
    BaseOrders,
}

impl Related<super::account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Accounts.def()
    }
}

impl Related<super::user_base_order::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BaseOrders.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_time.is_not_set() {
            self.created_time = Set(now.naive_utc());
        }
        if !self.last_modified_time.is_set() {
            self.last_modified_time = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
