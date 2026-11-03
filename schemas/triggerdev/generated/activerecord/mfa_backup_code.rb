class MfaBackupCode < ApplicationRecord
  self.table_name = "MfaBackupCode"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :code, presence: true, uniqueness: { scope: :userId }

  belongs_to :user, foreign_key: "userId", inverse_of: :backup_codes
end
