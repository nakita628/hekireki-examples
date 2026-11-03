class TwoFactorBackupCode < ApplicationRecord
  self.table_name = "two_factor_backup_code"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :used, default: false

  validates :code_hash, presence: true

  belongs_to :user, inverse_of: :two_factor_backup_codes
end
