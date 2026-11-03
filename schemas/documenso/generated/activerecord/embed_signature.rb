class EmbedSignature < ApplicationRecord
  self.table_name = "EmbedSignature"
  self.primary_key = "key"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :recipientId, presence: true
  validates :signature, presence: true

  belongs_to :organisation, foreign_key: "organisationId", inverse_of: :embed_signatures
end
