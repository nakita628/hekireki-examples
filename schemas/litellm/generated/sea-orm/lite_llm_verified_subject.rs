use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_VerifiedSubject")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub subject_id: String,
    pub issuer: String,
    pub tenant_id: String,
    pub oid: String,
    #[sea_orm(default_value = "human")]
    pub kind: String,
    pub user_id: Option<String>,
    #[sea_orm(default_value = "sso_interactive")]
    pub verified_via: String,
    pub verified_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_user_table::Entity",
        from = "Column::UserId",
        to = "super::lite_llm_user_table::Column::UserId",
        on_delete = "Cascade"
    )]
    User,
}

impl Related<super::lite_llm_user_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            subject_id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.verified_at.is_not_set() {
            self.verified_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
