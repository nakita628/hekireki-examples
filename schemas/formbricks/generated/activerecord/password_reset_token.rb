class PasswordResetToken < ApplicationRecord
  self.table_name = "PasswordResetToken"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :token_hash, presence: true, uniqueness: true
  validates :expires_at, presence: true
  validates :userId, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :password_reset_token
end
