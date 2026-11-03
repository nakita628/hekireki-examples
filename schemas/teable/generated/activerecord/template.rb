class Template < ApplicationRecord
  self.table_name = "template"

  attribute :id, default: -> { Cuid.generate }
  attribute :category_id, default: -> { [] }
  attribute :usage_count, default: 0
  attribute :visit_count, default: 0
  alias_attribute :updated_at, :last_modified_time

  validates :base_id, uniqueness: { allow_nil: true }
  validates :base_id, uniqueness: { allow_nil: true }
  validates :created_by, presence: true
  validates :order, presence: true
end
