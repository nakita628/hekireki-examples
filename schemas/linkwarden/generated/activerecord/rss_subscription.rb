class RssSubscription < ApplicationRecord
  self.table_name = "RssSubscription"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :url, presence: true
  validates :name, presence: true
  validates :ownerId, presence: true

  belongs_to :collection, foreign_key: "collectionId", inverse_of: :rss_subscriptions
end
