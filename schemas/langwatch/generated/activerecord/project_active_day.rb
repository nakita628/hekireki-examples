class ProjectActiveDay < ApplicationRecord
  self.table_name = "ProjectActiveDay"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :projectId, presence: true, uniqueness: { scope: :day }
  validates :day, presence: true
end
