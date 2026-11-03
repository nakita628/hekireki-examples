class PasswordResetToken < ApplicationRecord
  self.table_name = "PasswordResetToken"

  validates :identifier, presence: true, uniqueness: { scope: :token }
  validates :token, presence: true, uniqueness: true
  validates :expires, presence: true
end
