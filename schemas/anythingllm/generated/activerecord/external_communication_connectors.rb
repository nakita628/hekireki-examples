class ExternalCommunicationConnectors < ApplicationRecord
  self.inheritance_column = nil

  attribute :config, default: "{}"
  attribute :active, default: false
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :type, presence: true, uniqueness: true
end
