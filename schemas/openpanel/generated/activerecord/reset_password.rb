class ResetPassword < ApplicationRecord
  self.table_name = "reset_password"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :expiresAt, presence: true

  belongs_to :account, foreign_key: "accountId", inverse_of: :reset_passwords
end
