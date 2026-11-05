class Tags < ApplicationRecord
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :editedAt, PrismaDateTime.new
  alias_attribute :created_at, :createdAt

  validates :uuid, presence: true, uniqueness: true
  validates :userId, presence: true
  validates :name, presence: true

  has_and_belongs_to_many :files, class_name: "Files", join_table: "_filesTotags", foreign_key: "B",
                                  association_foreign_key: "A"
end
