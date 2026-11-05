class Files < ApplicationRecord
  self.inheritance_column = nil

  attribute :isS3, default: false
  attribute :isWatched, default: false
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :editedAt, PrismaDateTime.new
  attribute :quarantine, default: false
  alias_attribute :created_at, :createdAt

  validates :uuid, presence: true, uniqueness: true
  validates :name, presence: true
  validates :original, presence: true
  validates :type, presence: true
  validates :size, presence: true
  validates :hash, presence: true
  validates :ip, presence: true

  belongs_to :user, class_name: "Users", foreign_key: "userId", optional: true, inverse_of: :files
  has_one :quarantine_file, class_name: "QuarantineFiles", foreign_key: "fileId", primary_key: "uuid", inverse_of: :file,
                            dependent: :restrict_with_error
  has_and_belongs_to_many :albums, class_name: "Albums", join_table: "_albumsTofiles", foreign_key: "B",
                                   association_foreign_key: "A"
  has_and_belongs_to_many :tags, class_name: "Tags", join_table: "_filesTotags", foreign_key: "A",
                                 association_foreign_key: "B"
end
