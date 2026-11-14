class SsoCredential < ApplicationRecord
  self.table_name = "SsoCredential"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, presence: true
  validates :connectionId, presence: true
  validates :kind, presence: true
  validates :ciphertext, presence: true
end
