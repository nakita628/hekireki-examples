class BaseShare < ApplicationRecord
  self.table_name = "base_share"

  attribute :id, default: -> { Cuid.generate }
  attribute :enabled, default: true
  alias_attribute :updated_at, :last_modified_time

  validates :base_id, presence: true, uniqueness: { scope: :node_id, conditions: -> { where.not(node_id: nil) } }
  validates :share_id, presence: true, uniqueness: true
  validates :created_by, presence: true
end
