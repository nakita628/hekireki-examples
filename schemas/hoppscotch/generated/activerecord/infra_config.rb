class InfraConfig < ApplicationRecord
  self.table_name = "InfraConfig"

  attribute :id, default: -> { Cuid.generate }
  attribute :isEncrypted, default: false
  alias_attribute :updated_at, :updatedOn

  validates :name, presence: true, uniqueness: true
end
