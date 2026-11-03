use super::r#type::Type;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Order")]
pub struct Model {
    #[sea_orm(column_name = "accountId")]
    pub account_id: Option<String>,
    #[sea_orm(column_name = "accountUserId")]
    pub account_user_id: Option<String>,
    pub comment: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    pub currency: Option<String>,
    pub date: DateTime,
    pub fee: f64,
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub quantity: f64,
    #[sea_orm(column_name = "symbolProfileId")]
    pub symbol_profile_id: String,
    pub r#type: Type,
    #[sea_orm(column_name = "unitPrice")]
    pub unit_price: f64,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::account::Entity",
        from = "(Column::AccountId, Column::AccountUserId)",
        to = "(super::account::Column::Id, super::account::Column::UserId)"
    )]
    Account,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::symbol_profile::Entity",
        from = "Column::SymbolProfileId",
        to = "super::symbol_profile::Column::Id"
    )]
    SymbolProfile,
}

impl Related<super::account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Account.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::symbol_profile::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SymbolProfile.def()
    }
}

impl Related<super::tag::Entity> for Entity {
    fn to() -> RelationDef {
        super::order_to_tag::Relation::Tag.def()
    }
    fn via() -> Option<RelationDef> {
        Some(super::order_to_tag::Relation::Order.def().rev())
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
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
