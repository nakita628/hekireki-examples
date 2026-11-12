class Fonts < ApplicationRecord
  attribute :isLocal, default: false
  attribute :isSystem, default: false
  attribute :category, default: "sans-serif"
  attribute :sortOrder, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, length: { maximum: 255 }, uniqueness: true
  validates :name, uniqueness: true
  validates :displayName, presence: true, length: { maximum: 255 }
  validates :weights, exclusion: { in: [ nil ] }
  validates :category, length: { maximum: 50 }
end
