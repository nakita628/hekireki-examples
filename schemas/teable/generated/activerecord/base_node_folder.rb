class BaseNodeFolder < ApplicationRecord
  self.table_name = "base_node_folder"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :base_id, presence: true, uniqueness: { scope: :name }
  validates :name, presence: true
  validates :created_by, presence: true
end
