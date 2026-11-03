class Pixel < ApplicationRecord
  self.table_name = "pixel"
  self.primary_key = "pixel_id"

  validates :name, presence: true, length: { maximum: 100 }
  validates :slug, presence: true, length: { maximum: 100 }, uniqueness: true

  belongs_to :user, optional: true, inverse_of: :pixels
  belongs_to :team, optional: true, inverse_of: :pixels
end
