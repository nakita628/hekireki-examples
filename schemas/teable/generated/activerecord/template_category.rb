class TemplateCategory < ApplicationRecord
  self.table_name = "template_category"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :name, presence: true, uniqueness: true
  validates :created_by, presence: true
  validates :order, presence: true
end
