class SavedView < ApplicationRecord
  self.table_name = "SavedView"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :filters, default: -> { JSON.parse("{}") }
  attribute :order, default: 0
  attribute :kind, default: "v1-traces-filter"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :saved_views
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :saved_views
end
