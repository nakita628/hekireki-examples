class Media < ApplicationRecord
  validates :id, presence: true, uniqueness: { scope: :project_id }
  validates :sha_256_hash, presence: true, length: { maximum: 44 }, uniqueness: { scope: :project_id }
  validates :bucket_path, presence: true
  validates :bucket_name, presence: true
  validates :content_type, presence: true
  validates :content_length, presence: true

  belongs_to :project, inverse_of: :media
end
