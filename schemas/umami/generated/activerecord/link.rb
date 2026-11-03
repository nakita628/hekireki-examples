class Link < ApplicationRecord
  self.table_name = "link"
  self.primary_key = "link_id"

  validates :name, presence: true, length: { maximum: 100 }
  validates :url, presence: true, length: { maximum: 500 }
  validates :slug, presence: true, length: { maximum: 100 }, uniqueness: true

  belongs_to :user, optional: true, inverse_of: :links
  belongs_to :team, optional: true, inverse_of: :links
end
