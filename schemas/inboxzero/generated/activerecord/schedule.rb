class Schedule < ApplicationRecord
  self.table_name = "Schedule"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :emailAccountId, uniqueness: true
  validates :emailAccountId, uniqueness: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :digest_schedule
end
