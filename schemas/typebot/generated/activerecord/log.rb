class Log < ApplicationRecord
  self.table_name = "Log"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :status, presence: true
  validates :description, presence: true

  belongs_to :result, foreign_key: "resultId", inverse_of: :logs
end
