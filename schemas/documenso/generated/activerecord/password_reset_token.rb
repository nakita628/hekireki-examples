class PasswordResetToken < ApplicationRecord
  self.table_name = "PasswordResetToken"

  alias_attribute :created_at, :createdAt

  validates :token, presence: true, uniqueness: true
  validates :expiry, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :password_reset_tokens
end
