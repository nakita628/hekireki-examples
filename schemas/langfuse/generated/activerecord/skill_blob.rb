class SkillBlob < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :sha_256_hash, presence: true, length: { maximum: 44 }, uniqueness: { scope: :project_id }
  validates :content_type, presence: true
  validates :content_length, presence: true
  validates :content, presence: true

  belongs_to :project, inverse_of: :skill_blob
  has_many :files, class_name: "SkillFile", foreign_key: "blob_id", inverse_of: :blob, dependent: :restrict_with_error
end
