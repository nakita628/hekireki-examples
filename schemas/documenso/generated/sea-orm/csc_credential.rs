use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "CscCredential")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "providerId")]
    pub provider_id: String,
    #[sea_orm(column_name = "credentialId")]
    pub credential_id: String,
    #[sea_orm(column_name = "certCache")]
    pub cert_cache: Option<Vec<u8>>,
    #[sea_orm(column_name = "signatureAlgorithm")]
    pub signature_algorithm: String,
    #[sea_orm(column_name = "keyType")]
    pub key_type: String,
    #[sea_orm(column_name = "digestAlgorithm")]
    pub digest_algorithm: String,
    #[sea_orm(column_name = "keyLenBits")]
    pub key_len_bits: Option<i32>,
    #[sea_orm(column_name = "signAlgoParams")]
    pub sign_algo_params: Option<String>,
    #[sea_orm(column_name = "serviceTokenCiphertext")]
    pub service_token_ciphertext: Option<Vec<u8>>,
    #[sea_orm(column_name = "serviceTokenExpiresAt")]
    pub service_token_expires_at: Option<DateTime>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "recipientId")]
    pub recipient_id: i32,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::recipient::Entity",
        from = "Column::RecipientId",
        to = "super::recipient::Column::Id",
        on_delete = "Cascade"
    )]
    Recipient,
}

impl Related<super::recipient::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Recipient.def()
    }
}

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
