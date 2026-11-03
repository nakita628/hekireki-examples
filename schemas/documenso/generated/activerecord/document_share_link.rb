class DocumentShareLink < ApplicationRecord
  self.table_name = "DocumentShareLink"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :email, presence: true, uniqueness: { scope: :envelopeId }
  validates :slug, presence: true, uniqueness: true

  belongs_to :envelope, foreign_key: "envelopeId", inverse_of: :share_links
end
