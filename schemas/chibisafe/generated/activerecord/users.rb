class Users < ApplicationRecord
  attribute :enabled, default: true
  attribute :storageQuota, default: "0"
  attribute :passwordEditedAt, PrismaDateTime.new
  attribute :apiKeyEditedAt, PrismaDateTime.new
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :editedAt, PrismaDateTime.new
  alias_attribute :created_at, :createdAt

  validates :uuid, presence: true, uniqueness: true
  validates :username, presence: true, uniqueness: true
  validates :password, presence: true
  validates :apiKey, uniqueness: { allow_nil: true }

  has_many :files, class_name: "Files", foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :links, class_name: "ShortenedLinks", foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_and_belongs_to_many :roles, class_name: "Roles", join_table: "_rolesTousers", foreign_key: "B",
                                  association_foreign_key: "A"
end
