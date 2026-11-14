class AccountCredential < ApplicationRecord
  self.table_name = "AccountCredential"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :provider, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :account_credentials
end
