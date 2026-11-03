defmodule Umami.AppSetting do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          key: String.t(),
          value: String.t()
        }

  schema "app_setting" do
    field(:key, :string, primary_key: true)
    field(:value, :string)
  end
end