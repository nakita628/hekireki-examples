defmodule Umami.Report do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          name: String.t(),
          description: String.t(),
          parameters: map(),
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          user: Umami.User.t() | nil,
          website: Umami.Website.t() | nil
        }

  schema "report" do
    field(:id, :string, primary_key: true, source: :report_id)
    field(:type, :string)
    field(:name, :string)
    field(:description, :string)
    field(:parameters, :map)
    belongs_to(:user, Umami.User, foreign_key: :user_id, type: :string)
    belongs_to(:website, Umami.Website, foreign_key: :website_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end