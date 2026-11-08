class ShortURL < ApplicationRecord
  self.table_name = "ShortURL"

  attribute :clicks, default: 0
  alias_attribute :created_at, :createdAt

  validates :originalUrl, presence: true
  validates :shortUrlSlug, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :short_urls
end
