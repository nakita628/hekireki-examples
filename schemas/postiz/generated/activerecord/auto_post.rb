class AutoPost < ApplicationRecord
  self.table_name = "AutoPost"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :title, presence: true
  validates :onSlot, inclusion: { in: [ true, false ] }
  validates :syncLast, inclusion: { in: [ true, false ] }
  validates :url, presence: true
  validates :lastUrl, presence: true
  validates :active, inclusion: { in: [ true, false ] }
  validates :addPicture, inclusion: { in: [ true, false ] }
  validates :generateContent, inclusion: { in: [ true, false ] }
  validates :integrations, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :auto_post
end
