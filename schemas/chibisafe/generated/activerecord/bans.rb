class Bans < ApplicationRecord
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :ip, presence: true
end
