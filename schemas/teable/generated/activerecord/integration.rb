class Integration < ApplicationRecord
  self.table_name = "integration"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :resource_id, presence: true, uniqueness: true
  validates :config, presence: true
  validates :type, presence: true
end
