class Shortcode < ApplicationRecord
  self.table_name = "Shortcode"

  alias_attribute :updated_at, :updatedOn

  validates :id, uniqueness: { scope: :creatorUid, conditions: -> { where.not(creatorUid: nil) } }
  validates :request, exclusion: { in: [ nil ] }

  belongs_to :user, foreign_key: "creatorUid", optional: true, inverse_of: :shortcodes
end
