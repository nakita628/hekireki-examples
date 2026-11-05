class ShortenedLinks < ApplicationRecord
  self.table_name = "shortenedLinks"

  attribute :views, default: 0
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :uuid, presence: true, uniqueness: true
  validates :identifier, presence: true, uniqueness: true
  validates :destination, presence: true

  belongs_to :user, class_name: "Users", foreign_key: "userId", optional: true, inverse_of: :links
end
