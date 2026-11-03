defmodule Rallly.NonprofitApplication do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          organization_name: String.t(),
          website: String.t(),
          email_domain: String.t(),
          status: atom(),
          reason: String.t() | nil,
          model: String.t() | nil,
          inserted_at: DateTime.t(),
          space: Rallly.Space.t() | nil,
          user: Rallly.User.t() | nil
        }

  schema "nonprofit_applications" do
    field(:organization_name, :string)
    field(:website, :string)
    field(:email_domain, :string)
    field(:status, Ecto.Enum, values: [:approved, :rejected, :failed])
    field(:reason, :string)
    field(:model, :string)
    belongs_to(:space, Rallly.Space, foreign_key: :space_id)
    belongs_to(:user, Rallly.User, foreign_key: :user_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end