class Collaborator < ApplicationRecord
  self.table_name = "collaborator"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :role_name, presence: true
  validates :resource_type, presence: true, uniqueness: { scope: %i[resource_id principal_id principal_type] }
  validates :resource_id, presence: true
  validates :principal_id, presence: true
  validates :principal_type, presence: true
  validates :created_by, presence: true
end
