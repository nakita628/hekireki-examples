class BrowserExtensionApiKeys < ApplicationRecord
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :lastUpdatedAt

  validates :key, presence: true, uniqueness: true

  belongs_to :user, class_name: "Users", optional: true, inverse_of: :browser_extension_api_keys
end
