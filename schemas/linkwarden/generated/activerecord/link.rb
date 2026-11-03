class Link < ApplicationRecord
  self.table_name = "Link"
  self.inheritance_column = nil

  attribute :name, default: ""
  attribute :type, default: "url"
  attribute :description, default: ""
  attribute :clientSide, default: false
  attribute :aiTagged, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :created_by, class_name: "User", foreign_key: "createdById", optional: true, inverse_of: :created_links
  belongs_to :collection, foreign_key: "collectionId", inverse_of: :links
  has_many :highlight, foreign_key: "linkId", inverse_of: :link, dependent: :destroy
  has_and_belongs_to_many :pinned_by, class_name: "User", join_table: "_PinnedLinks", foreign_key: "A",
                                      association_foreign_key: "B"
  has_and_belongs_to_many :tags, join_table: "_LinkToTag", foreign_key: "A", association_foreign_key: "B"
end
