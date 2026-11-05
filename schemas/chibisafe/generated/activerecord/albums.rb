class Albums < ApplicationRecord
  attribute :zippedAt, PrismaDateTime.new
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :editedAt, PrismaDateTime.new
  attribute :nsfw, default: false
  alias_attribute :created_at, :createdAt

  validates :uuid, presence: true, uniqueness: true
  validates :userId, presence: true
  validates :name, presence: true

  has_and_belongs_to_many :files, class_name: "Files", join_table: "_albumsTofiles", foreign_key: "A",
                                  association_foreign_key: "B"
end
