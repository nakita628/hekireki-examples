class Analytics < ApplicationRecord
  self.table_name = "Analytics"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  validates :key, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :analytics
end
