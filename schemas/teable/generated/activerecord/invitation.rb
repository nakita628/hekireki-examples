class Invitation < ApplicationRecord
  self.table_name = "invitation"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :type, presence: true
  validates :role, presence: true
  validates :invitation_code, presence: true
  validates :create_by, presence: true
end
