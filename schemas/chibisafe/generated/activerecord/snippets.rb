class Snippets < ApplicationRecord
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :editedAt, PrismaDateTime.new
  alias_attribute :created_at, :createdAt

  validates :uuid, presence: true, uniqueness: true
  validates :identifier, presence: true, uniqueness: true
  validates :name, presence: true
  validates :content, presence: true
  validates :language, presence: true
end
