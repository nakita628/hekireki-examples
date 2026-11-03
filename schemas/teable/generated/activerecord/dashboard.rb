class Dashboard < ApplicationRecord
  self.table_name = "dashboard"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :name, presence: true
  validates :base_id, presence: true
  validates :created_by, presence: true
end
